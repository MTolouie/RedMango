import { Fragment } from "react";
import { NavLink } from "react-router-dom";
const mangoLogo = require("../../Assets/Images/mango.png");

const Header = () => {
  return (
    <Fragment>
      <nav className="navbar navbar-expand-lg navbar-dark bg-dark">
        <NavLink className="nav-link" to="/">
          <img src={mangoLogo} className="m-1" style={{ height: "40px" }} />
        </NavLink>
        <button
          className="navbar-toggler"
          type="button"
          data-toggle="collapse"
          data-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>

        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav mr-auto">
            <li className="nav-item">
              <NavLink className="nav-link" aria-current="page" to="/">
                Home
              </NavLink>
            </li>

            <li className="nav-item">
              <NavLink
                className="nav-link"
                to="/ShoppingCart"
                aria-current="page"
              >
                <i className="bi bi-cart"></i>Shopping Cart
              </NavLink>
            </li>

            <li className="nav-item dropdown">
              <NavLink
                className="nav-link dropdown-toggle"
                to="#"
                id="navbarDropdown"
                role="button"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              >
                Admin Panel
              </NavLink>
              <div className="dropdown-menu" aria-labelledby="navbarDropdown">
                <NavLink className="dropdown-item" to="#">
                  Action
                </NavLink>
                <NavLink className="dropdown-item" to="#">
                  Another action
                </NavLink>
                <div className="dropdown-divider"></div>
                <NavLink className="dropdown-item" to="#">
                  Something else here
                </NavLink>
              </div>
            </li>
          </ul>
        </div>
      </nav>
    </Fragment>
  );
};

export default Header;
