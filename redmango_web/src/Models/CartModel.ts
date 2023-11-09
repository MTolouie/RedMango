import {CartDetailsModel} from "./index";

export default interface CartModel {
    cartId: number
    userId: string
    cartSum: number
    isFinally: boolean
    createDate: string
    cartDetails: CartDetailsModel[]
    stripePaymentIntentId: any
    clientSecret: any
  }