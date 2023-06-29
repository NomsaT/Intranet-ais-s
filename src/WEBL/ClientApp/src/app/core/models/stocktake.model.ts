export interface Stocktake {
    id: number;
    stockId: number;
    plantLocationId: number;
    storeId: number;
    currentQty: number;
    capturedQty: number;
    recount: boolean;
    stocktakeDate: Date;
    userId: number;
    stock: string;
    location: string;
    store: string;
}