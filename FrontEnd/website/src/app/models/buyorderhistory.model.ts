export interface BuyOrderHistory {
    customerId: number;
    cryptoName: string;
    buyPrice: number;
    buyDate: Date;
    quantity: number;
    total: number;
}