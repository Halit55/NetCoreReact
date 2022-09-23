import axios from "axios";
import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import departmentDto from '../dtos/departmentDto';
import { toast } from 'react-toastify'

const user=JSON.parse(localStorage.getItem("user")); 
const headers = {
  'Content-Type': 'application/json',
  'Accept': 'application/json',
  'Authorization': `Bearer `+user?.token,
};

const initialState:departmentDto[]=[];

export const getDepartmentAsync= createAsyncThunk('getDepartmentAsync', async () => {
  try {  
    const { data } = await axios.get<departmentDto[]>('https://localhost:44346/department/GetAllAsync', { headers });    
    return data;
  } catch (error) {
    toast("Bir hata oluştu.: " + error);   
  }
}
)


export const userSlice = createSlice({
  name: 'department',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(getDepartmentAsync.fulfilled, (state, action) => {
      state = action.payload;      
      return state;
    })
  }
})



export default userSlice.reducer