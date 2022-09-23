
import { useEffect } from 'react';
import { useAppDispatch, useAppSelector } from '../reduxToolkit/store'
import { getUserAsync, deleteUserAsync } from '../reduxToolkit/userSlice';
import { useState } from 'react';
import AddUser from './addUser';
import UpdateUser from './updateUser';
import userDto from '../dtos/userDto';
import { toast } from 'react-toastify'

export default function Users() {
  const dispatch = useAppDispatch();
  const [addModalShow, setAddModalShow] = useState(false);
  const [updateModalShow, setUpdateModalShow] = useState(false);
  const [currentUser, setUser] = useState(null);


  const users = useAppSelector(state => state.user);
  useEffect(() => {
    dispatch(getUserAsync());
  }, [])

  

  const deleteUser = (userId: string) => {
    const response = dispatch(deleteUserAsync(userId));
    response.then(result => {
      toast("Üye başarıyla silindi!");  
      dispatch(getUserAsync());       
    });
  }

  const updateUser = (user: userDto) => {   
    setUser({
      ...currentUser,
      user
    })
  }

  const userList = users.map((user) => {
    return <tr key={user.userId}>
      <td>{user.userName}</td>
      <td>{user.password}</td>
      <td>{user.department.departmentName}</td>
      <td>
        <button className="btn btn-danger btn-sm" onClick={() => deleteUser(user.userId)}>Sil</button>
        <button className="btn btn-warning btn-sm" onClick={() => { setUpdateModalShow(true); updateUser(user) }} >Güncelle</button>
      </td>
    </tr>
  })



  return (
    <div>
      <table className="table">
        <thead className="table table-dark">
          <tr>
            <th scope="col">Adı</th>
            <th scope="col">Soyadı</th>
            <th scope="col">Department</th>
            <th scope="col">İşlemler  <button className="btn btn-primary btn-sm" onClick={() => setAddModalShow(true)}>Ekle</button></th>
          </tr>
        </thead>
        <tbody>
          {userList}
        </tbody>
      </table>
      <AddUser show={addModalShow} onHide={() => setAddModalShow(false)} />
      <UpdateUser currentUser={currentUser} show={updateModalShow} onHide={() => setUpdateModalShow(false)} />
    </div>
  );


}

