import React, { ReactNode } from "react";
import { ApiResponseModel, MenuItemModel } from "../../Models";
import { useQuery } from "react-query";
import { fetchMenuItems, queryClient } from "../../Utilities";
import { LoadingIndicator } from "../UI";
import {ErrorBlock} from "../UI/index";
import MenuItem from "./MenuItem";

const MenuItemsList = () => {
  const { data, isLoading, isError, error } = useQuery<MenuItemModel[], Error>({
    queryKey: ["menuItems"],
    queryFn: async ({ signal }) =>
      await fetchMenuItems(signal, "GetAllMenuItems"),
    staleTime: 10000,
  });

  let content: ReactNode;

  
  if (isError) {
    content = <ErrorBlock title="Something Went Wrong" message={error.message} />;
  }
  
  if (isLoading) {
    content = <LoadingIndicator />;
  }
  
  if (data) {
    content = data.map((menuItem: MenuItemModel, index: number) => (
      <MenuItem key={index} menuItem={menuItem} />
      ));
}

return <div className="container row ">{content}</div>;
};


export const menuItemsListLoader = ({request}:{request:Request})=>{
  return queryClient.fetchQuery({
    queryKey: ["menuItems"],
    queryFn: ({ signal }) => fetchMenuItems(signal,"GetAllMenuItems"),
  });
  
}

export default MenuItemsList;
