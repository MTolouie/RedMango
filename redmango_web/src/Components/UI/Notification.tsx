import { useEffect } from "react";
import { NotificationModel } from "../../Models";
import { useDispatch } from "react-redux";
import { RemoveNotification } from "../../storage/redux/notification-slice";

const Notification : React.FC<NotificationModel> = (props) => {
  const dispatch = useDispatch<any>();
  
  let specialClasses = '';

  if (props.status === 'Error') {
    specialClasses = "error";
  }
  if (props.status === 'Success') {
    specialClasses = "success";
  }

  if (props.status === 'Loading') {
    specialClasses = "Loading";
  }
  
  let cssClasses = `notification ${specialClasses}`;

  useEffect(()=>{
    setTimeout(()=>{
      dispatch(RemoveNotification());
      
    },5000);  
  },[]);


  return (
    <section className={cssClasses}>
      <h2>{props.title && props.title}</h2>
      <p>{props.message && props.message}</p>
    </section>
  );
};

export default Notification;