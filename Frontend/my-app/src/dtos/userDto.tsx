
import departmentDto from './departmentDto';

export default interface userDto{
    userId:string;
    userName:string;
    password:string; 
    departmentId:string;
    token:string;   
    department?:departmentDto;
}