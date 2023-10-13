import { NotificationModel } from "../../Models";

const Notification : React.FC<NotificationModel> = (props) => {
  let specialClasses = '';

  if (props.status === 'Error') {
    specialClasses = "error";
  }
  if (props.status === 'Success') {
    specialClasses = "success";
  }

  const cssClasses = `notification ${specialClasses}`;

  return (
    <section className={cssClasses}>
      <h2>{props.title}</h2>
      <p>{props.message}</p>
    </section>
  );
};

export default Notification;