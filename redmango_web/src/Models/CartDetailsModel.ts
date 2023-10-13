import MenuItemModel from "./MenuItemModel";

export default interface CartDetailsModel {
    DetailId: number;
    CartId: number;
    MenuItems: MenuItemModel[];
    Quantity: number;
    price: number;
  }