import { createSlice } from "@reduxjs/toolkit";
import { NotificationModel } from "../../Models";

const initialNotification:NotificationModel = {
  title : "",
  message:"",
  status : ""
};
const notificationSlice = createSlice({
  name: "notification",
  initialState: initialNotification,
  reducers: {
    showNotification(state, action) {
      state.title = action.payload.title;
      state.status = action.payload.status;
      state.message = action.payload.message;
    },
    hideNotification(state) {
      state.title = "";
      state.status = "";
      state.message = "";
      // state = initialNotification;
    },
  },
});

export const RemoveNotification = (
) => {
  return async (dispatch: any) => {
    dispatch(
      notificationActions.hideNotification()
    );
  };
};

export const notificationActions = notificationSlice.actions;
export default notificationSlice.reducer;
