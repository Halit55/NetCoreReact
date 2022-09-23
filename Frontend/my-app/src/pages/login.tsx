
import React from 'react';
import { useAppDispatch } from '../reduxToolkit/store';
import { loginAsync } from '../reduxToolkit/loginSlice';
import "../css/login.css";
import { toast } from 'react-toastify';
import logo from '../images/logo.png';


export default function Login() {

    const dispatch = useAppDispatch();

    const initialState = { username: '', password: '' };

    const [loginData, setForm] = React.useState<{ username: string; password: string }>(initialState);

    const clearState = () => {
        setForm({ ...initialState });
      };


    const handleChange = (event: React.FormEvent<HTMLInputElement>) => {
        setForm({
            ...loginData,
            [event.currentTarget.name]: event.currentTarget.value
        })
    }


    const submitForm = (e: React.SyntheticEvent) => {      
        if (loginData.password !== '' && loginData.username !== '') {
            e.preventDefault();
            const response = dispatch(loginAsync(loginData));
            response.then(result => {
                if (result.payload != null) {
                    localStorage.setItem("user", JSON.stringify(result.payload));
                    window.location.href = "/home"; 
                 }
            });
        }
        else {         
            clearState();  
           toast("Kullanıcı adı ve şifre boş geçilemez!");
        }
    }

    return (
        <div className="container h-100">            
            <div className="d-flex justify-content-center h-100">
                <div className="user_card">
                    <div className="d-flex justify-content-center">
                        <div className="brand_logo_container">
                        <img src={logo} alt="logo"/>
                        </div>
                    </div>
                    <div className="d-flex justify-content-center form_container">
                        <form>
                            <div className="input-group mb-3">
                                <div className="input-group-append">
                                    <span className="input-group-text"><i className="fas fa-user"></i></span>
                                </div>
                                <input onChange={handleChange} value={loginData.username} autoComplete="off" required className='form-control' type="text" name='username' placeholder='username' />
                            </div>
                            <div className="input-group mb-2">
                                <div className="input-group-append">
                                    <span className="input-group-text"><i className="fas fa-key"></i></span>
                                </div>
                                <input onChange={handleChange} value={loginData.password} autoComplete="off" required className='form-control' type="text" name='password' placeholder='password' />
                            </div>
                            {/* <div className="form-group">
                                <div className="custom-control custom-checkbox">
                                    <input type="checkbox" className="custom-control-input" id="customControlInline" />
                                    <label className="custom-control-label">Beni Hatırla</label>
                                </div>
                            </div> */}
                            <div className="d-flex justify-content-center mt-3 login_container">
                                <button type="submit" onClick={submitForm} className="btn btn-primary login_btn">Giriş Yap</button>
                            </div>
                        </form>
                    </div>
                    {/* <div className="mt-4">
                        <div className="d-flex justify-content-center links">
                            <a href="#">Üye Ol</a>
                        </div>
                        <div className="d-flex justify-content-center links">
                            <a href="#">Şifremi Unuttum</a>
                        </div>
                    </div> */}
                </div>
            </div>
        </div>
    )
}
