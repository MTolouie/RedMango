import { Fragment } from "react";
import { MenuItemsList } from "../Components/MenuItem";

const HomePage = () => {
  return (
    <Fragment>
      <div className="container p-2">
        <MenuItemsList />
      </div>
    </Fragment>
  );
};

export default HomePage;

