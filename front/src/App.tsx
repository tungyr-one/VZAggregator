import './App.css';
import TripList from './components/triplist/TripList';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Register from './components/register/register/Register';
import Login from './components/login/Login';
import 'react-toastify/dist/ReactToastify.css';
import UserDashboard from './components/user-dashboard/UserDashboard';
import AdminDashboard from './components/admin-dashboard/AdminDashboard';
import { AuthProvider } from './contexts/AuthContext';
import Navbar from './components/navbar/navbar';

const App: React.FC = () => {

  return (
    <div className="App">
    <BrowserRouter> 
      <AuthProvider>
        <Navbar />
          <Routes>
            <Route path="/" element={<TripList />} />
            <Route path="/register" element={<Register />} />
            <Route path="/login" element={<Login />} />
            <Route path="/user-account" element={<UserDashboard />} />
            <Route path="/admin-account" element={<AdminDashboard />} />
          </Routes>
        </AuthProvider>
      </BrowserRouter>
    </div>
  );
};
export default App;
