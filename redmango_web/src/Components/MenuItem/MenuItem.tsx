import { Fragment } from "react";
import { MenuItemModel } from "../../Models";
import { Link } from "react-router-dom";
import { useDispatch } from "react-redux";
import { sendCartData } from "../../storage/redux/cart-slice";
const MenuItem: React.FC<{menuItem:MenuItemModel}> = (props) => {
  const dispatch = useDispatch<any>();

  return <div className="col-md-4 col-12 p-4">
  <div
    className="card"
    style={{ boxShadow: "0 1px 7px 0 rgb(0 0 0 / 50%)" }}
  >
    <div className="card-body pt-2">
      <div className="row col-10 offset-1 p-4">
        <Link to={`/MenuItemDetails/${props.menuItem.id}`}>
        <img
          src={props.menuItem.image}
          style={{ borderRadius: "50%" }}
          alt={props.menuItem.name}
          className="w-100 mt-5 image-box"
        />
        </Link>
      </div>

     {props.menuItem.specialTag != null && props.menuItem.specialTag.length >0 && (
         <i
         className="bi bi-star btn btn-success"
         style={{
           position: "absolute",
           top: "15px",
           left: "15px",
           padding: "5px 10px",
           borderRadius: "3px",
           outline: "none !important",
           cursor: "pointer",
         }}
       >
         &nbsp; SPECIAL
       </i>
     )}

      <i
        className="bi bi-cart-plus btn btn-outline-danger"
        style={{
          position: "absolute",
          top: "15px",
          right: "15px",
          padding: "5px 10px",
          borderRadius: "3px",
          outline: "none !important",
          cursor: "pointer",
        }}
        onClick={()=>{ dispatch(sendCartData("AddToCart","9ab525a9-0103-4345-ba04-10a01f1ab3a6",1,props.menuItem))}}
      ></i>

      <div className="text-center">
      <Link to={`/MenuItemDetails/${props.menuItem.id}`} style={{textDecoration:"none",color:"green"}}> <p className="card-title m-0 text-success fs-3">{props.menuItem.name}</p></Link>
        <p className="badge bg-secondary" style={{ fontSize: "12px" }}>
        {props.menuItem.category}
        </p>
      </div>
      <p className="card-text" style={{ textAlign: "center" }}>
      {props.menuItem.description}
      </p>
      <div className="row text-center">
        <h4>${props.menuItem.price}</h4>
      </div>
    </div>
  </div>
</div>
};
export default MenuItem;
