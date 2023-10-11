import { SuccessBlockModel } from "../../Models";

const SuccessBlock: React.FC<SuccessBlockModel> = ({ title, message }) => {
  return (
    <div className="success-block">
      <div className="success-block-icon">!</div>
      <div className="success-block-text">
        <h2>{title}</h2>
        <p>{message}</p>
      </div>
    </div>
  );
};
export default SuccessBlock;
