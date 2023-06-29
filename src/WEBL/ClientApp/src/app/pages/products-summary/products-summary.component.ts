import { Component, Inject, OnInit, ViewChild } from "@angular/core";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { DxDataGridComponent } from "devextreme-angular";
import * as AspNetData from "devextreme-aspnet-data-nojquery";
import { exportDataGrid } from "devextreme/excel_exporter";
import notify from "devextreme/ui/notify";
import { Workbook } from "exceljs";
import saveAs from "file-saver";
import { AuthenticationService } from "../../core/services/auth.service";
import { GlobalMethodsService } from "../../core/services/global-methods.service";
import { popupAutoSelectService } from "../../core/services/popupAutoSelect.service";
import { BarcodesService } from "../../core/services/barcodes.service";
import { StockService } from "../../core/services/stock.service";
import QRCode from "qrcode";

import * as pdfMake from "pdfmake/build/pdfmake";
import * as pdfFonts from "pdfmake/build/vfs_fonts";
import { Product } from "./product-model";

@Component({
  selector: "app-products-summary",
  templateUrl: "./products-summary.component.html",
  styleUrls: ["./products-summary.component.scss"],
})
export class ProductsSummaryComponent implements OnInit {
  dataSource: any;
  DepartmentForeignDataSource: any;
  isVisible: string;
  transactions: Array<[]>;
  statData: Array<[]>;
  isActive: string;
  defaultVisible = false;
  defaultVisible2 = false;
  disableCostCenter = true;
  departmentDropDown: any;
  baseUrl: any;
  profitCenterID = 0;
  rowIndex = 0;
  pageName: string = "Product Summary";
  event: any;
  CancelDownload = true;
  popupDownloadConfirmVisible = false;
  popupVisibleRemove = false;
  popupVisibleRemoveProd = false;
  popupVisible = false;
  allowModify = false;
  showStock = false;
  showProduct = false;
  productItemsNum:any;
  productStockNum:any;
  primaryProduct: number;

  noDownloadButtonOptions: any = {
    text: "No",
    onClick: (e) => {
      this.popupDownloadConfirmVisible = false;
      this.CancelDownload = true;
    },
  };

  yesDownloadButtonOptions = {
    text: "Yes",
    onClick: () => {
      this.popupDownloadConfirmVisible = false;
      const workbook = new Workbook();
      const worksheet = workbook.addWorksheet("Main sheet");
      exportDataGrid({
        component: this.event.component,
        worksheet: worksheet,
      }).then(function () {
        workbook.xlsx.writeBuffer().then(function (buffer: BlobPart) {
          saveAs(
            new Blob([buffer], { type: "application/octet-stream" }),
            "Products-List.xlsx"
          );
        });
      });
    },
  };

  ComponentRemove: any = {
    text: "Remove",
    type: "success",
    useSubmitBehavior: false,
    onClick: () => {
      this.productStockNum = false;
      this.showStock = false;
      this.stockService.RemoveAllProductStock(this.primaryProduct).subscribe(data => { });
      this.popupVisibleRemove = false;
    }
  }

  CancelRemove: any = {
    text: "Cancel",
    type: "danger",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleRemove = false;
      this.productStockNum = true;
    }
  }

  ProdComponentRemove: any = {
    text: "Remove",
    type: "success",
    useSubmitBehavior: false,
    onClick: () => {
      this.productItemsNum = false;
      this.showProduct = false;
      this.stockService.RemoveAllProductItem(this.primaryProduct).subscribe(data => { });
      this.popupVisibleRemoveProd = false;
    }
  }

  ProdCancelRemove: any = {
    text: "Cancel",
    type: "danger",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisibleRemoveProd = false;
      this.productItemsNum = true;
    }
  }

  buttonOptions: any = {
    icon: "plus",
    hint: "Add a new department",
    type: "default",
    useSubmitBehavior: false,
    onClick: () => {
      this.popupVisible = true;
    },
  };

  @ViewChild("content") content;
  @ViewChild('product', { static: false }) product: DxDataGridComponent;
  @ViewChild('productstock', { static: false }) productstock: DxDataGridComponent;
  @ViewChild('productitem', { static: false }) productitem: DxDataGridComponent;
  qrSvg: string;

  base64logo: string;
  PAGE_WIDTH = 238.11;
  HEIGHT = 153.071;
  PAGE_HEIGHT = this.PAGE_WIDTH + this.HEIGHT;
  PAGE_MARGIN = 15;

  constructor(
    @Inject("BASE_URL") baseUrl: string,
    private modalService: NgbModal,
    private globalMethodsService: GlobalMethodsService,
    private authService: AuthenticationService,
    private autoSelect: popupAutoSelectService,
    private barcodesService: BarcodesService,
    private stockService: StockService
  ) {
    this.baseUrl = baseUrl;
    this.rowIndex = 0;
    this.dataSource = {
      store: AspNetData.createStore({
        key: "id",
        loadUrl: baseUrl + "api/Products",
        updateUrl: baseUrl + "api/Products",
        insertUrl: baseUrl + "api/Products",
        deleteUrl: baseUrl + "api/Products",
        onInserted: () => {
          this.globalMethodsService.ToastInsertSuccessMessage(this.pageName);
        },
        onUpdated: () => {
          this.globalMethodsService.ToastUpdateSuccessMessage(this.pageName);
        },
        onRemoved: () => {
          this.globalMethodsService.ToastDeleteSuccessMessage(this.pageName);
        },
        /*onBeforeSend: (method, param) => {
          param.headers = {
            'Authorization': 'Bearer ' + this.authService.getToken()
          };
        }*/
      }),
      paginate: true,
      sort: [{ selector: "name", desc: false }],
    };

    this.DepartmentForeignDataSource = {
      store: AspNetData.createStore({
        key: "id",
        loadUrl: baseUrl + "api/Department",
        /* onBeforeSend: (method, param) => {
           param.headers = {
             'Authorization': 'Bearer ' + this.token
           };
         }*/
      }),
      sort: [{ selector: "abbAndName", desc: false }],
      paginate: true,
    };

    this.allowModify = this.authService.HavePermission(24);
  }

  ngOnInit() {
    const attribute = document.body.getAttribute("data-layout");

    this.isVisible = attribute;
    const vertical = document.getElementById("layout-vertical");
    if (vertical != null) {
      vertical.setAttribute("checked", "true");
    }
    if (attribute == "horizontal") {
      const horizontal = document.getElementById("layout-horizontal");
      if (horizontal != null) {
        horizontal.setAttribute("checked", "true");
      }
    }
  }

  toggleDefault() {
    this.defaultVisible = !this.defaultVisible;
  }

  toggleDefault2() {
    this.defaultVisible2 = !this.defaultVisible2;
  }

  selectionChanged(e) {
    e.component.collapseAll(-1);
    e.component.expandRow(e.currentSelectedRowKeys[0]);
  }

  rowChanged(e) {
    e.component.collapseAll(-1);
    this.showStock = false;
    this.showProduct = false;
    this.productStockNum = 0;
    this.productItemsNum = 0;
    this.primaryProduct = e.key;

    this.stockService.GetProductStock(e.key).subscribe(
      (data) => {
        if (data != null && data != undefined && data > 0) {
          this.showStock = true;
          this.productStockNum = data;
        } else {
          this.showStock = false;
          this.productStockNum = 0;
        }
      },
      (subError) => {
        notify(subError, "error", 5000);
      }
    );
    this.stockService.GetProductItem(e.key).subscribe(
      (data) => {
        if (data != null && data != undefined && data > 0) {
          this.showProduct = true;
          this.productItemsNum = data;
        } else {
          this.showProduct = false;
          this.productItemsNum = 0;
        }
      },
      (subError) => {
        notify(subError, "error", 5000);
      }
    );    
  }

  EditStart(e) {
    this.rowIndex = this.product.instance.getRowIndexByKey(e.data.id);
  }

  SavingStart(e) {
    this.rowIndex = 0;
  }

  onExporting(e) {
    this.event = e;
    this.popupDownloadConfirmVisible = true;
    if (this.CancelDownload) {
      e.cancel = true;
    }
  }

  onExported(e) {
    notify("Successfully Downloaded", "success", 5000);
  }

  onHidden(e) {
    this.product.instance.getDataSource().reload();
    this.autoSelect.GetNewestDepartment().subscribe(
      (data) => {
        this.product.instance.cellValue(this.rowIndex, "departmentId", data);
      },
      (subError) => {
        notify(subError.error, "error", 5000);
      }
    );
  }

  barcodeOnClick(e) {
    if (e.rowType === "data" && e.column.caption == "Print Barcode") {
      this.barcodesService
        .PrintingProduct([
          { Id: e.data.id, Code: e.data.productCode, Barcode: e.data.barcode },
        ])
        .subscribe(
          (data) => {
            if (data != null && data != undefined) {
              notify("Barcode Printed", "success", 5000);
            }
          },
          (subError) => {
            notify(subError, "error", 5000);
          }
        );
    }

    if (e.rowType === "data" && e.column.caption == "Download Barcode") {
      const product: Product = e.data;
      this.generateQRCode(product);
      notify("Product PDF ready for download or printing", "success", 5000);
    }
  }

  generateQRCode(product: Product) {
    let data = product.barcode.substring(3) + "," + product.productCode;
    QRCode.toDataURL(data, { errorCorrectionLevel: "M" }, function (err, url) {
      let docContent1 = {
        content: [
          {
            text: "PRODUCT INFORMATION\n\n\n",
            style: "header",
          },
          {
            text: [
              {
                text: "Name: " + product.productName,
                fontSize: 20,
                bold: true,
                alignment: "center",
              },
              "\n",
              {
                text: "Barcode: " + product.barcode,
                fontSize: 20,
                bold: true,
                alignment: "center",
              },
              "\n",
              {
                text: "Description: " + product.description,
                fontSize: 15,
                bold: false,
                alignment: "center",
              },
              "\n",
              "\n\n",
            ],
          },
          {
            // if you specify both width and height - image will be stretched
            image: url,
            width: 400,
            style: "qr",
          },
        ],
        styles: {
          header: {
            fontSize: 18,
            bold: true,
            alignment: "center",
          },
          bigger: {
            fontSize: 15,
            italics: true,
          },
          qr: {
            alignment: "center",
          },
        },
      };
      let docContent = {
        content: [
          // {
          //   text: "PRODUCT INFORMATION\n\n\n",
          //   style: "header",
          // },
          {
            text: product.productName,
            style: "name",
          },
          {
            // if you specify both width and height - image will be stretched
            image: url,
            width: 400,
            style: "qr",
          },
          {
            text: [
              {
                text: product.barcode,
                fontSize: 20,
                bold: true,
                alignment: "center",
              },
              "\n",
              // {
              //   text: "Description: " + product.description,
              //   fontSize: 15,
              //   bold: false,
              //   alignment: "center",
              // },
              // "\n",
              // "\n\n",
            ],
          },
        ],
        styles: {
          header: {
            fontSize: 18,
            bold: true,
            alignment: "center",
          },
          bigger: {
            fontSize: 15,
            italics: true,
          },
          qr: {
            alignment: "center",
          },
          name: {
            fontSize: 24,
            bold: true,
            alignment: "center",
          },
        },
      };
      pdfMake.createPdf(docContent, null, null, pdfFonts.pdfMake.vfs).open();
    });
  }

  handleValueChangeStock(e) {    
    if (e.value) {
      this.showStock = true;
    } else {   
      this.popupVisibleRemove = true;
      /*this.showStock = false;
      this.stockService.RemoveAllProductStock(this.primaryProduct).subscribe(data => {});*/
    }
  }

  handleValueChangeProduct(e) {
    if (e.value) {
      this.showProduct = true;
    } else {
      this.popupVisibleRemoveProd = true;
    /*  this.showProduct = false;
      this.stockService.RemoveAllProductItem(this.primaryProduct).subscribe(data => {});*/
    }
  }
}
