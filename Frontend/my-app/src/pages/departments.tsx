
import { useEffect } from 'react';
import { useAppDispatch, useAppSelector } from '../reduxToolkit/store'
import { getDepartmentAsync } from '../reduxToolkit/departmentSlice';


export default function Departments() {
    const dispatch = useAppDispatch();

    const departments = useAppSelector(state => state.department);
    useEffect(() => {
        dispatch(getDepartmentAsync());
    }, [])



    const departmentList = departments.map((department) => {
        return <tr key={department.departmentId}>
            <td>{department.departmentId}</td>
            <td>{department.departmentName}</td>            
        </tr>
    })



    return (
        <div>
            <table className="table">
                <thead>
                    <tr>
                        <th scope="col">No</th>
                        <th scope="col">Department</th>                        
                    </tr>
                </thead>
                <tbody>
                    {departmentList}
                </tbody>
            </table>
        </div>
    );


}

