import { Fragment } from "react";
import { Header,Footer } from "../Components/Layout";
// import { useRouteError } from "react-router-dom";
const ErrorPage = () => {

    // const error  = useRouteError() ;
    // let title = "An Error Occurred!";
    // let message = "Something Went Wrong";

    // if(error.status === 404){
    //     message = "Url Or Page Not Found";
    //     title = "Not Found";
    // }
    
    // if(error.status === 500){
    //     message = error.data.message;
    // }

  return <Fragment>
    {/* <PageContent title={title}>
        <p>{message} {error.status}</p>
    </PageContent> */}
    <Header />
    <div className="alert alert-danger text-center container mt-5"><p>SomeThing Went Wrong</p></div>
    <Footer />
  
  </Fragment>;

};

export default ErrorPage;
