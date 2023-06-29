import { MenuItem } from './menu.model';

export const MENU: MenuItem[] = [
  {
    id: 1,
    label: 'MENUITEMS.DASHBOARDS.TEXT',
    icon: 'bx-home-circle',
    link: '/dashboard'
  },
  {
    id: 2,
    label: 'MENUITEMS.MASTERLISTS.TEXT',
    icon: 'bx-align-left',
    subItems: [
      {
        id: 3,
        label: 'MENUITEMS.MASTERLISTS.LIST.CUSTOMER',
        link: '/customers',
        parentId: 2,
        permissionId: [2,3]
      },
      {
        id: 4,
        label: 'MENUITEMS.MASTERLISTS.LIST.SUPPLIER',
        link: '/suppliers',
        parentId: 2,
        permissionId: [4,5]
      },
      {
        id: 5,
        label: 'MENUITEMS.MASTERLISTS.LIST.DEPARTMENT',
        link: '/department',
        parentId: 2,
        permissionId: [8, 9]      
      },
      {
        id: 6,
        label: 'MENUITEMS.MASTERLISTS.LIST.DEPARTMENTMANAGERS',
        link: '/department-managers',
        parentId: 2,
        permissionId: [6, 7]        
      }
    ]
  },
  {
    id: 7,
    label: 'MENUITEMS.STOCKMANAGEMENT.TEXT',
    icon: 'bx-store',
    subItems: [
      {
        id: 8,
        label: 'MENUITEMS.STOCKMANAGEMENT.LIST.STOCK',
        link: '/stocklist',
        parentId: 7,
        permissionId: [12,13]
      },
      {
        id: 9,
        label: 'MENUITEMS.STOCKMANAGEMENT.LIST.TRANSFERSTOCK',
        link: '/stock-transfer',
        parentId: 7,
        permissionId: [55]
      },
      {
        id: 10,
        label: 'MENUITEMS.STOCKMANAGEMENT.LIST.CORRECTINGSTOCK',
        link: '/stock-correction',
        parentId: 7,
        permissionId: [16,17]
      }
    ]
  },
  {
    id: 11,
    label: 'MENUITEMS.PRICELOOKUP.TEXT',
    icon: 'bx-purchase-tag-alt',
    link: '/price-lookup',
    badge: {
      variant: 'danger',
      text: ''
    },
    permissionId: [18, 19, 20, 21, 22]
  },
  {
    id: 12,
    label: 'MENUITEMS.PRODUCTS.TEXT',
    icon: 'bx-box',
    link: '/products',
    permissionId: [23,24]
  },  
  {
    id: 13,
    label: 'MENUITEMS.PURCHASE.TEXT',
    icon: 'bx bx-archive',
    subItems: [
      {
        id: 14,
        label: 'MENUITEMS.ORDERS.TEXT',
        icon: 'bx-hash',
        link: '/orders',
        parentId: 13,
        permissionId: [58, 59]
      },
      {
        id: 15,
        label: 'MENUITEMS.GRN.TEXT',
        icon: 'bx-note',
        link: '/grn',
        parentId: 13,
        permissionId: [85, 86]
      },
      {
        id: 16,
        label: 'MENUITEMS.INVOICES.TEXT',
        icon: 'bxs-file-plus',
        link: '/invoices',
        parentId: 13,
        permissionId: [72, 73]
      },
      {
        id: 17,
        label: 'MENUITEMS.QUOTATIONS.TEXT',
        link: '/quotation',
        parentId: 13,
        permissionId: [109, 110]
      }
    ]
  },
  {
    id: 18,
    label: 'MENUITEMS.REPORTS.TEXT',
    icon: 'bx bx-receipt',
    subItems: [
      {
        id: 19,
        label: 'MENUITEMS.REPORTS.LIST.STOCKREPORT',
        link: '/stock-report',
        parentId: 18,
        permissionId: [25]
      },
      {
        id: 20,
        label: 'MENUITEMS.REPORTS.LIST.ORDERSREPORT',
        link: '/approved-orders-report',
        parentId: 18,
        permissionId: [69]
      },
      {
        id: 21,
        label: 'MENUITEMS.REPORTS.LIST.SERVICEREPORT',
        link: '/service-items-report',
        parentId: 18,
        permissionId: [87]
      },
      {
        id: 22,
        label: 'MENUITEMS.REPORTS.LIST.ONCEOFFREPORT',
        link: '/once-off-items-report',
        parentId: 18,
        permissionId: [88]
      },
      {
        id: 23,
        label: 'MENUITEMS.REPORTS.LIST.GLVATREPORT',
        link: '/gl-vat-report',
        parentId: 18,
        permissionId: [89]
      },
      {
        id: 24,
        label: 'MENUITEMS.REPORTS.LIST.CHARTREPORT',
        link: '/charts',
        parentId: 18,
        permissionId: [101]
      },
      {
        id: 25,
        label: 'MENUITEMS.REPORTS.LIST.STOCKRECIPES',
        link: '/stock-recipes',
        parentId: 18,
        permissionId: [103]
      }
    ]
  }, 
  {
    id: 26,
    label: 'MENUITEMS.FILEMANAGEMENT.TEXT',
    icon: 'bx bx-file',
    subItems: [
      {
        id: 27,
        label: 'MENUITEMS.FILEMANAGEMENT.LIST.ADDFILES',
        link: '/file-management',
        parentId: 26,
        permissionId: [62, 63]
      },
      {
        id: 28,
        label: 'MENUITEMS.FILEMANAGEMENT.LIST.IMPORTFILES',
        link: '/import-files',
        parentId: 26,
        permissionId: [64, 65]
      }
    ]
  },
  {
    id: 29,
    label: 'MENUITEMS.USERMANAGEMENT.TEXT',
    icon: 'bx-align-left',
    subItems: [
      {
        id: 30,
        label: 'MENUITEMS.USERMANAGEMENT.LIST.USERS',
        link: '/users',
        parentId: 29,
        permissionId: [23, 27, 28, 29]
      },
      {
        id: 31,
        label: 'MENUITEMS.USERMANAGEMENT.LIST.ALLOCATIONPC',
        link: '/allocate-users',
        parentId: 29,
        permissionId: [30,31]
      },
      {
        id: 32,
        label: 'MENUITEMS.USERMANAGEMENT.LIST.ROLETEMP',
        link: '/roles',
        parentId: 29,
        permissionId: [32,33]
      },
      {
        id: 33,
        label: 'MENUITEMS.USERMANAGEMENT.LIST.ASSIGNROLES',
        link: '/assign-roles',
        parentId: 29,
        permissionId: [34,35]
      },
      {
        id: 34,
        label: 'MENUITEMS.USERMANAGEMENT.LIST.PERMISSIONS',
        link: '/permissions',
        parentId: 29,
        permissionId: [36,37]
      },      
    ]
  },
  {
    id: 35,
    label: 'MENUITEMS.OTHERS.TEXT',
    icon: 'bx-info-circle',
    subItems: [
      {
        id: 36,
        label: 'MENUITEMS.OTHERS.LIST.PHONEBOOK',
        link: '/phonelist',
        parentId: 35,
        permissionId: [90]
      },
      {
        id: 37,
        label: 'MENUITEMS.OTHERS.LIST.BIRTHDAY',
        link: '/birthdays',
        parentId: 35,
        permissionId: [91]
      }
    ]
  },
  {
    id: 38,
    label: 'MENUITEMS.SETTINGS.TEXT',
    icon: 'bx-cog',
    subItems: [
      {
        id: 39,
        label: 'MENUITEMS.SETTINGS.LIST.SYSTEMCONFIG',
        link: '/system-config',
        parentId: 38,
        subItems: [
          {
            id: 40,
            label: 'MENUITEMS.SETTINGS.LIST.STOCKCATEGORY',
            link: '/stock-category',
            parentId: 38,
            permissionId: [40, 41]
          },
          {
            id: 41,
            label: 'MENUITEMS.SETTINGS.LIST.UOM',
            link: '/unit-of-measurement',
            parentId: 38,
            permissionId: [42, 43]
          },
          {
            id: 42,
            label: 'MENUITEMS.SETTINGS.LIST.PAYMENTMETHOD',
            link: '/payment-method',
            parentId: 38,
            permissionId: [44, 45]
          },
          {
            id: 43,
            label: 'MENUITEMS.SETTINGS.LIST.EMPLOYEEPOSITION',
            link: '/employee-position',
            parentId: 38,
            permissionId: [46, 47]
          },
          {
            id: 44,
            label: 'MENUITEMS.SETTINGS.LIST.STOCKGROUP',
            link: '/stock-group',
            parentId: 38,
            permissionId: [51, 52]
          },
          {
            id: 45,
            label: 'MENUITEMS.SETTINGS.LIST.STORAGETYPE',
            link: '/storage-type',
            parentId: 38,
            permissionId: [56, 57]
          },
          {
            id: 46,
            label: 'MENUITEMS.SETTINGS.LIST.STORETYPE',
            link: '/store-type',
            parentId: 38,
            permissionId: [48, 49]
          },
          {
            id: 47,
            label: 'MENUITEMS.SETTINGS.LIST.PLANTLOCATION',
            link: '/plant-location',
            parentId: 38,
            permissionId: [70, 71]
          },
          {
            id: 48,
            label: 'MENUITEMS.SETTINGS.LIST.GLCODE',
            link: '/gl-code',
            parentId: 38,
            permissionId: [74, 75]
          },
          {
            id: 49,
            label: 'MENUITEMS.SETTINGS.LIST.COSTTYPE',
            link: '/cost-type',
            parentId: 38,
            permissionId: [76, 77]
          },
          {
            id: 50,
            label: 'MENUITEMS.SETTINGS.LIST.BANKNAME',
            link: '/bank-name',
            parentId: 38,
            permissionId: [80, 81]
          },
          {
            id: 53,
            label: 'MENUITEMS.SETTINGS.LIST.VAT',
            link: '/update-vat',
            parentId: 38,
            permissionId: [84]
          }
        ]
      }
    ]
  },
  {
    id: 54,
    label: 'MENUITEMS.STORES.TEXT',
    icon: 'bxs-dashboard',
    link: '/stores/dashboard',
    permissionId: [92]
  },
  {
    id: 55,
    label: 'MENUITEMS.FIN.TEXT',
    icon: 'bx-dollar',
    link: '/fin-dashboard',
    permissionId: [105]
  },
  {
    id: 56,
    label: 'MENUITEMS.PRODUCTIONDASHBOARD.TEXT',
    icon: 'bxs-dashboard',
    link: '/production-dashboard',
    permissionId: [117]
  },
  {
    id: 57,
    label: 'MENUITEMS.LOGOUT.TEXT',
    icon: 'bx-log-in',
    link: '/account/login'
  }
];

