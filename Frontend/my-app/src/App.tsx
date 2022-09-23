
import './App.css';
import Login from './pages/login';
import Home from './pages/home';
import Users from './pages/users';
import Departments from './pages/departments';
import Navbar from './pages/navbar';
import { BrowserRouter, Routes, Route} from 'react-router-dom'
import { ToastContainer } from 'react-toastify';


export default function App() {
  const user = localStorage.getItem("user"); 
  return (
    <div className="App">   
      <ToastContainer />
      <BrowserRouter>
        {user != null ? <Navbar /> : <Login />}
        <Routes>
          {/* <Route path={'/login'} element={<Login />} /> */}
          <Route path={'/home'} element={<Home />} />
          <Route path={'/users'} element={<Users />} />
          <Route path={'/departments'} element={<Departments />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}


