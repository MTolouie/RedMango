import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap/dist/js/bootstrap.js";
import "bootstrap-icons/font/bootstrap-icons.css";
import { QueryClientProvider } from 'react-query';
import { queryClient } from './Utilities';
import { Provider } from 'react-redux';
import { store } from './storage';
const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);


root.render(
  <React.StrictMode>
  <QueryClientProvider client={queryClient}>
  <Provider store={store}>
    <App />
    </Provider>
  </QueryClientProvider>
  </React.StrictMode>
);
