import { createSlice } from "@reduxjs/toolkit";
import { CartDetailsModel, MenuItemModel } from "../../Models/index";
import { addToCart } from "../../Utilities";
import { notificationActions } from "./notification-slice";

const initialCartState: CartDetailsModel = {
  CartId: 0,
  DetailId: 0,
  price: 0,
  Quantity: 0,
  MenuItems: [],
};

const cartSlice = createSlice({
  name: "cart",
  initialState: initialCartState,
  reducers: {
    addToCart(state, action) {
      const existingItemIndex = state.MenuItems.findIndex(
        (item) => item.id === action.payload.id
      );
      const existingItem = state.MenuItems[existingItemIndex];
      // const updatedTotalAmount = existingItem.total + (action.payload.price * action.payload.quantity );
      if (existingItem) {
        state.Quantity += action.payload.quantity;
      } else {
        state.MenuItems.push(action.payload);
      }
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
        }))
    }
  };
};

// export const fetchCartData = () => {
//   return async (dispatch) => {
//     const fetchData = async () => {
//       const response = await fetch(
//         "https://task-4792d-default-rtdb.firebaseio.com/cart.json"
//       );

//       if (!response.ok) {
//         throw new Error("Could not fetch cart data!");
//       }

//       const data = await response.json();

//       return data;
//     };

//     try {
//       const cartData = await fetchData();
//       dispatch(
//         cartActions.replaceCart({
//           items: cartData.items || [],
//         })
//       );
//     } catch (error) {
//       dispatch(
//         notificationActions.showNotification({
//           status: "error",
//           title: "Error!",
//           message: "Fetching cart data failed!",
//         })
//       );
//     }
//   };
// };

export const cartActions = cartSlice.actions;
export default cartSlice.reducer;
