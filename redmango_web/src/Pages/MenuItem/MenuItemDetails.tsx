import { useQuery } from "react-query";
import { MenuItemModel } from "../../Models";
import { queryClient } from "../../Utilities";
import { fetchMenuItem } from "../../Utilities/menuItemHttps";
import {
  LoaderFunction,
  json,
  redirect,
  useLoaderData,
  useNavigate,
  useParams,
} from "react-router-dom";
import { ReactNode, useState } from "react";
import { ErrorBlock, LoadingIndicator } from "../../Components/UI";

type myParams = {
  menuItemId: string;
};

const MenuItemDetails = () => {
  const params = useParams<myParams>();
  const [quantity, setQuantity] = useState(1);
  const { data, isLoading, isError, error } = useQuery<MenuItemModel, Error>({
    queryKey: ["menuItem", { menuItemId: params.menuItemId }],
    queryFn: ({ signal }) =>
      fetchMenuItem(signal, params.menuItemId || "0", "GetMenuItem"),
  });

  const navigate = useNavigate();

  const handleQuantity = (counter: number) => {
    let newQuantity = quantity + counter;
    if(newQuantity === 0){
      newQuantity = 1;
    }
    setQuantity(newQuantity);
    return;
  };

  let content: ReactNode;

  if (isLoading) {
    content = <LoadingIndicator />;
  }

  if (isError) {
    content = (
      <ErrorBlock title="Something Went Wrong" message={error.message} />
    );
  }

  if (data) {
    content = (
      <div className="row">
        <div className="col-7">
          <h2 className="text-success">{data.name}</h2>
          <span>
            <span
              className="badge text-bg-dark pt-2"
              style={{ height: "40px", fontSize: "20px" }}
            >
              {data.category}
            </span>
          </span>
          {data.specialTag.length > 0 && data.specialTag != null && (
            <span>
              <span
                className="badge text-bg-light pt-2"
                style={{ height: "40px", fontSize: "20px" }}
              >
                {data.specialTag}
              </span>
            </span>
          )}
          <p style={{ fontSize: "20px" }} className="pt-2">
            {data.description}
          </p>
          <span className="h3">${data.price}</span> &nbsp;&nbsp;&nbsp;
          <span
            className="pb-2  p-3"
            style={{ border: "1px solid #333", borderRadius: "30px" }}
          >
            <i
              onClick={() => handleQuantity(-1)}
              className="bi bi-dash p-1"
              style={{ fontSize: "25px", cursor: "pointer" }}
            ></i>
            <span className="h3 mt-3 px-3">{quantity}</span>
            <i
              onClick={() => handleQuantity(1)}
              className="bi bi-plus p-1"
              style={{ fontSize: "25px", cursor: "pointer" }}
            ></i>
          </span>
          <div className="row pt-4">
            <div className="col-5">
              <button
                onClick={() => navigate("../")}
                className="btn btn-success form-control"
              >
                Add to Cart
              </button>
            </div>

            <div className="col-5 ">
              <button className="btn btn-secondary form-control">
                Back to Home
              </button>
            </div>
          </div>
        </div>
        <div className="col-5">
          <img
            src={data.image}
            width="100%"
            style={{ borderRadius: "50%" }}
            alt={data.name}
          ></img>
        </div>
      </div>
    );
  }
  return <div className="container pt-4 pt-md-5">{content}</div>;
};

// type loaderParams = {
//   params:myParams,
//   request:Request
// }

export const menuItemDetialsLoader: LoaderFunction = ({ request, params }) => {
  if (params === undefined || params.menuItemId === undefined) {
    return redirect("../");
  }
  return queryClient.fetchQuery({
    queryKey: ["menuItem", { menuItemId: params.menuItemId }],
    queryFn: ({ signal }) =>
      fetchMenuItem(signal, params.menuItemId || "0", "GetMenuItem"),
  });
};

export default MenuItemDetails;
