import { Fragment } from "react";
import { Footer,Header, RootLayout } from "./Components/Layout";
import {HomePage,ErrorPage ,MenuItemDetails}from "./Pages/index";
import {
  RouterProvider,
  createBrowserRouter,
} from "react-router-dom";


const router = createBrowserRouter([
  {
    path: "/",
    element: <RootLayout />,
    errorElement:<ErrorPage/>,
    children: [
      { path: "/", element: <HomePage /> },
      { path: "/MenuItemDetails/:menuItemId", element: <MenuItemDetails /> },
    ],
  }
]);






function App() {
  return  <RouterProvider router={router} />
}

export default App;
