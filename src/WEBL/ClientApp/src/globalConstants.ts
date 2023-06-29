// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const currencyZAR = { type: "currency", precision: 2 };
//TODO: Update Item Type (Internal Order Items)
export const Listed = 5;
export const OnceOff = 6;
export const Service = 7;
//TODO: Update Stock Category
//export const EnumRecipe = 2013;
export const EnumRecipe = 1009;
//TODO: Update denied status for orders
export const approvedStatus = 1;
export const deniedStatus = 3;
export const pendingStatus = 2;
export const reviewStatus = 5;
export const pendingMonitoredStatus = 4;
export const draftStatus = 6;
export const closeStatus = 7;

//TODO:QUOTATION statuses
export const completed = 1;
export const draft = 2;
//TODO: Update Supplier Location
export const EnumLocalLocation = 1;
export const taxPattern = /^[0-9]{10}$/;
export const contactPattern = /^[0-9]{10,11}$/;
export const postalPattern = /^[0-9]+$/;
export const accountPattern = /^[0-9]+$/;
export const branchPattern = /^[0-9]+$/;
export const worklandlinePattern = /^([0-9]{10,11})?$/;

export const idPattern = /^[0-9]{13}$/;

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
