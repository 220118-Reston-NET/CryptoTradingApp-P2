export interface SellOrderHistory {
    customerId: number;
    cryptoName: string;
    sellPrice: number;
    sellDate: Date;
    quantity: number;
    total: number;
}