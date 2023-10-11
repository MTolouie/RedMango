import { useMutation, useQuery } from "react-query";
import { ApiResponseModel, MenuItemModel } from "../../Models";
import { addToCart, queryClient } from "../../Utilities";
import { fetchMenuItem } from "../../Utilities/menuItemHttps";
import {
  LoaderFunction,
  redirect,
  useNavigate,
  useParams,
} from "react-router-dom";
import { ReactNode, useState } from "react";
import { ErrorBlock, LoadingIndicator } from "../../Components/UI";
import SuccessBlock from "../../Components/UI/SuccessBlock";

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

  let mutaionContent: ReactNode;
  let content: ReactNode;

  const {
    mutate,
    isError: isMutationError,
    error: mutationError,
  } = useMutation<ApiResponseModel, Error>({
    mutationFn: () =>
      addToCart(
        "AddToCart",
        "9ab525a9-0103-4345-ba04-10a01f1ab3a6",
        quantity,
        data?.id || 0
      ),
  });

  const navigate = useNavigate();

  const handleQuantity = (counter: number) => {
    let newQuantity = quantity + counter;
    if (newQuantity === 0) {
      newQuantity = 1;
    }
    setQuantity(newQuantity);
    return;
  };

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
                onClick={() => mutate()}
                className="btn btn-success form-control"
              >
                Add to Cart
              </button>
            </div>

            <div className="col-5 ">
              <button
                className="btn btn-secondary form-control"
                onClick={() => navigate("../")}
              >
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

  if (isMutationError) {
    mutaionContent = (
      <ErrorBlock
        title="Something Went Wrong"
        message={mutationError.message || "Could Not Add To Cart"}
      />
    );
  } else {
    mutaionContent = (
      <SuccessBlock title="Success" message={"Added Successfuly"} />
    );
  }
  return (
    <div className="container pt-4 pt-md-5">
      {mutaionContent}
      {content}
    </div>
  );
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
