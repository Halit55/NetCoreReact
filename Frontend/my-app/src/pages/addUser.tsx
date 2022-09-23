
import { useEffect } from 'react';
import { useAppDispatch, useAppSelector } from '../reduxToolkit/store'
import { getDepartmentAsync } from '../reduxToolkit/departmentSlice';
import Container from 'react-bootstrap/Container';
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import React from 'react';
import { getUserAsync,createUserAsync } from '../reduxToolkit/userSlice';
import userDto from '../dtos/userDto';
import { toast } from 'react-toastify'

export default function AddUser(props: any) {

    const dispatch = useAppDispatch(); 

    const departments = useAppSelector(state => state.department);
    useEffect(() => {
        dispatch(getDepartmentAsync());
    }, [])


    const departmentList = departments.map((department, key) => {
        return <option key={key} value={department.departmentId}>{department.departmentName}</option>
    })

    const initialState = {
        userId: '0', userName: '', password: '', departmentId: '', token: ''
      }
   
    const [userData, setForm] = React.useState<userDto>(initialState);

    const clearState = () => {
        setForm({ ...initialState });
      };


    const handleChange = (event: React.FormEvent<HTMLInputElement>) => {
        setForm({
            ...userData,
            [event.currentTarget.name]: event.currentTarget.value
        })
    }

    const selectHandleChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setForm({
            ...userData,
            [event.currentTarget.name]: event.currentTarget.value
        })
    }

    const submitForm = (e: React.SyntheticEvent) => {
        debugger;
        if (userData.userName!="" && userData.password!="" && userData.departmentId!="") {
            e.preventDefault();
            const response = dispatch(createUserAsync(userData));
            response.then(result => {
                toast("Üye başarıyla eklendi!");
                dispatch(getUserAsync());
                props.onHide(); 
                clearState();               
            });
        }
        else {       
            toast("Üye kaydı için kullanıcı adı,şifre ve departman zorunludur!");
       }      
    }

    return (
        <Modal {...props} aria-labelledby="contained-modal-title-vcenter">
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Kullanıcı Ekleme
                </Modal.Title>
            </Modal.Header>
            <Modal.Body className="show-grid">
                <Container>
                    <Row>
                        <div>
                            <form>
                                <div className="form-group row">
                                    <label className="col-sm-3 col-form-label">Kullanıcı Adı</label>
                                    <div className="col-sm-9">
                                        <input onChange={handleChange} value={userData.userName} autoComplete="on" required className='form-control' type="text" name='userName' />
                                    </div>
                                </div>
                                <div className="form-group row">
                                    <label className="col-sm-3 col-form-label">Şifre</label>
                                    <div className="col-sm-9">
                                        <input onChange={handleChange} value={userData.password} autoComplete="on" required className='form-control' type="text" name='password' />
                                    </div>
                                </div>
                                <div className="form-group row">
                                    <label className="col-sm-3 col-form-label">Departman</label>
                                    <div className="col-sm-9">
                                        <select className="form-select" onChange={selectHandleChange} value={userData.departmentId} name='departmentId' required>
                                            <option value="" selected>Seçiniz</option>
                                            {departmentList}
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <div className="row">
                                    <div className="col-sm-9"></div>
                                    <div className="col-sm-3">
                                        <button type='submit' onClick={submitForm} className="btn btn-sm btn-primary flex-left">Kaydet</button>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </Row>
                </Container>
            </Modal.Body>
        </Modal>
    );
}

