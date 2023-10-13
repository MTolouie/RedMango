import { Fragment } from "react";
import {  RootLayout } from "./Components/Layout";
import { HomePage, ErrorPage, MenuItemDetails } from "./Pages/index";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { menuItemsListLoader } from "./Components/MenuItem/index";
import { menuItemDetialsLoader } from "./Pages/MenuItem";
import { useSelector } from "react-redux";
import { Notification } from "./Components/UI";
const router = createBrowserRouter([
  {
    path: "/",
    element: <RootLayout />,
    errorElement: <ErrorPage />,
    children: [
      { path: "/", element: <HomePage />, loader: menuItemsListLoader },
      {
        path: "/MenuItemDetails/:menuItemId",
        element: <MenuItemDetails />,
        loader: menuItemDetialsLoader,
      },
    ],
  },
]);


function App() {
  const notification : any = useSelector<any>((state) => state.notification);
  return <Fragment>
    {notification.title && (
        <Notification
          title={notification.title}
          status={notification.status}
          message={notification.message}
        />
      )}
    <RouterProvider router={router} />
  </Fragment>;
}

export default App;
