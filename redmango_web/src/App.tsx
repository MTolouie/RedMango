import { Fragment } from "react";
import { Footer, Header, RootLayout } from "./Components/Layout";
import { HomePage, ErrorPage, MenuItemDetails } from "./Pages/index";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import { menuItemsListLoader } from "./Components/MenuItem/index";
import { menuItemDetialsLoader } from "./Pages/MenuItem";

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
  return <RouterProvider router={router} />;
}

export default App;
