import { createSlice } from "@reduxjs/toolkit";
import { CartDetailsModel, CartModel, MenuItemModel } from "../../Models/index";
import { addToCart, fetchUserCart } from "../../Utilities";
import { notificationActions } from "./notification-slice";

// const initialCartState: CartDetailsModel = {
//   CartId: 0,
//   DetailId: 0,
//   price: 0,
//   Quantity: 0,
//   MenuItems: [],
// };

const initialCartState: CartModel = {
  cartId: 0,
  userId: "",
  cartSum: 0,
  isFinally: false,
  createDate: "",
  cartDetails: [],
  stripePaymentIntentId: null,
  clientSecret: null,
};

const cartSlice = createSlice({
  name: "cart",
  initialState: initialCartState,
  reducers: {
    addToCart(state, action) {
      const existingItemIndex = state.cartDetails.findIndex(
        (cartDetail) =>
          cartDetail.MenuItems.findIndex(
            (item) => item.id === action.payload.id
          ) !== -1
      );
      const existingItem = state.cartDetails.find(
        (item) => item.MenuItems[existingItemIndex]
      );
      // const updatedTotalAmount = existingItem.total + (action.payload.price * action.payload.quantity );
      if (existingItem) {
        state.cartDetails.forEach((element) => {
          element.Quantity += action.payload.quantity;
        });
      } else {
        state.cartDetails.forEach((element) => {
          element.MenuItems.push(action.payload);
        });
      }
    },
    getUserCart(state, action) {
      state = action.payload;
    },
  },
});

export const sendCartData = (
  action: string,
  userId: string,
  quantity: number,
  menuItem: MenuItemModel
) => {
  return async (dispatch: any) => {
    dispatch(
      notificationActions.showNotification({
        status: "Loading",
        title: "Please Wait",
        message: "Trying To Add To The Cart",
      })
    );
    try {
      const result = await addToCart(action, userId, quantity, menuItem.id);
      dispatch(
        cartActions.addToCart({ quantity: quantity, id: menuItem, menuItem })
      );
      dispatch(
        notificationActions.showNotification({
          status: "Success",
          title: "Success!",
          message: "Adding To Cart Was Successful!",
        })
      );
      return result;
    } catch (error) {
      dispatch(
        notificationActions.showNotification({
          status: "Error",
          title: "Failed!",
          message: "Something Went Wrong!",
        })
      );
    }
  };
};

export const fetchCartData = (userId: string) => {
  return async (dispatch: any) => {
    dispatch(
      notificationActions.showNotification({
        status: "Loading",
        title: "Please Wait",
        message: "Trying To Get Cart Data",
      })
    );
    try {
      const result = await fetchUserCart(userId);
      dispatch(cartActions.getUserCart({ cart: result }));
      dispatch(
        notificationActions.showNotification({
          status: "Success",
          title: "Success!",
          message: "Fetching Cart Was Successful!",
        })
      );
      return result;
    } catch (error) {
      dispatch(
        notificationActions.showNotification({
          status: "Error",
          title: "Failed!",
          message: "Something Went Wrong!",
        })
      );
    }
  };
};

export const cartActions = cartSlice.actions;
export default cartSlice.reducer;
