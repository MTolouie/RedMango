import { QueryClient } from "react-query";
import {fetchMenuItems} from "./menuItemHttps";
import {addToCart,fetchUserCart} from "./cartHttps";

const queryClient = new QueryClient();
export {fetchMenuItems,queryClient,addToCart,fetchUserCart};