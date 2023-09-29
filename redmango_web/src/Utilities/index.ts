import { QueryClient } from "react-query";
import {fetchMenuItems} from "./menuItemHttps";

const queryClient = new QueryClient();
export {fetchMenuItems,queryClient};