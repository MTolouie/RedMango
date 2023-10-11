import { QueryClient } from "react-query";
import {fetchMenuItems} from "./menuItemHttps";
import {addToCart} from "./cartHttps";

const queryClient = new QueryClient();
export {fetchMenuItems,queryClient,addToCart};