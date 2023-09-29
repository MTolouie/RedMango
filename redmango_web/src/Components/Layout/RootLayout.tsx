import { Fragment } from "react";
import { Outlet } from "react-router-dom";
import { Header, Footer } from "./index";
const RootLayout = () => {
  return (
    <Fragment>
      <Header />
      <Outlet />
      <Footer />
    </Fragment>
  );
};

export default RootLayout;
