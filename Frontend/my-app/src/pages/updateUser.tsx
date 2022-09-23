import { useEffect } from 'react';
import { useAppDispatch, useAppSelector } from '../reduxToolkit/store'
import { getDepartmentAsync } from '../reduxToolkit/departmentSlice';
import Container from 'react-bootstrap/Container';
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import React from 'react';
import { getUserAsync, updateUserAsync } from '../reduxToolkit/userSlice';
import userDto from '../dtos/userDto';
import { useState } from 'react';
import { toast } from 'react-toastify'

export default function UpdateUser(props: any) {

    const dispatch = useAppDispatch();

    let currentuser: userDto = props.currentUser?.user;

    let [userData, setForm] = useState<userDto>(currentuser);

    if (currentuser?.userId !== userData?.userId) {
        userData = currentuser
    }

    const departments = useAppSelector(state => state.department);
    useEffect(() => {
        dispatch(getDepartmentAsync());
    }, [])


    const departmentList = departments.map((department, key) => {
        return <option key={key} value={department.departmentId} selected={props.currentUser?.user.departmentId === department?.departmentId ? true : false}>{department.departmentName}</option>
    })


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
        e.preventDefault();
        if (userData?.userId !== "" && userData?.departmentId !== "" && userData?.userName !== "" && userData?.password !== "") {
            const response = dispatch(updateUserAsync(userData));
            response.then(result => {
                toast("Üye başarıyla güncellendi!");
                dispatch(getUserAsync());
                props.onHide();                                           
            });
        }
        else {
            toast("Üye güncellemek için kullanıcı adı,şifresi ve departmanı zorunludur!");
        }
    }

    return (
        <Modal {...props} aria-labelledby="contained-modal-title-vcenter">
            <Modal.Header closeButton>
                <Modal.Title id="contained-modal-title-vcenter">
                    Kullanıcı Güncelleme
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
                                        <input onChange={handleChange} value={userData?.userName} autoComplete="on" required className='form-control' type="text" name='userName' />
                                    </div>
                                </div>
                                <div className="form-group row">
                                    <label className="col-sm-3 col-form-label">Şifre</label>
                                    <div className="col-sm-9">
                                        <input onChange={handleChange} value={userData?.password} autoComplete="on" required className='form-control' type="text" name='password' />
                                    </div>
                                </div>
                                <div className="form-group row">
                                    <label className="col-sm-3 col-form-label">Departman</label>
                                    <div className="col-sm-9">
                                        <select className="form-control" onChange={selectHandleChange} name='departmentId' required>
                                            <option value="">Seçiniz</option>
                                            {departmentList}
                                        </select>
                                    </div>
                                </div>
                                <br />
                                <div className="row">
                                    <div className="col-sm-9"></div>
                                    <div className="col-sm-3">
                                        <button onClick={submitForm} className="btn btn-sm btn-primary flex-left">Güncelle</button>
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

