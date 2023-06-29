import { MenuItem } from './menu.model';

export const MENUSTORE: MenuItem[] = [
  {
    id: 1,
    label: 'MENUITEMS.MENU.TEXT',
    isTitle: true
  },
  {
    id: 2,
    label: 'MENUITEMS.DASHBOARDS.TEXT',
    icon: 'bxs-dashboard',
    link: '/stores/dashboard'
  },
  {
    id: 3,
    label: 'MENUITEMS.CAPTURE.TEXT',
    icon: 'bx-hash',
    link: '/grn',
    permissionId: [93, 94]
  },
  {
    id: 4,
    label: 'MENUITEMS.BARCODE.TEXT',
    icon: 'bx-barcode',
    link: '/stores/print-barcode',
    badge: {
      variant: 'info',
      text: 'MENUITEMS.BARCODE.BADGE'
    },
    permissionId: [95, 96]
  },
  {
    id: 5,
    label: 'MENUITEMS.REBARCODE.TEXT',
    icon: 'bx-barcode',
    link: '/stores/reprint-barcode',
    permissionId: [97, 98]
  },
  {
    id: 6,
    label: 'MENUITEMS.BINOVERVIEW.TEXT',
    icon: 'bx-align-left',
    link: '/stores/bin-overview',
    permissionId: [131]
  },
  {
    id: 7,
    label: 'MENUITEMS.REPORTS.TEXT',
    icon: 'bx-align-left',
    subItems: [
      {
        id: 8,
        label: 'MENUITEMS.REPORTS.LIST.BVERIFREPORT',
        link: '/stores/barcode-verification-report',
        parentId: 6,
        permissionId: [102]
      }
    ]
  },
  {
    id: 9,
    label: 'MENUITEMS.ACS.TEXT',
    icon: 'bxs-dashboard',
    link: '/dashboard',
    permissionId: [99]
  },
  {
    id: 10,
    label: 'MENUITEMS.LOGOUT.TEXT',
    icon: 'bx-log-in',
    link: '/account/login'
  }
];
