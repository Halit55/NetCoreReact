import axios from "axios";
import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import userDto from '../dtos/userDto';
import { toast } from 'react-toastify'

const user = JSON.parse(localStorage.getItem("user"));
const headers = {
  'Content-Type': 'application/json',
  'Accept': 'application/json',
  'Authorization': `Bearer ` + user?.token,
};

const initialState: userDto[] = [];


export const createUserAsync = createAsyncThunk('createUserAsync', async (user: userDto) => {
  try {
    const response = await axios.post<userDto>('https://localhost:44346/user/AddAsync', user, { headers });
    return response.data;
  } catch (error) {  
    toast("Bir hata oluştu.: " + error);
  }
}
)

export const updateUserAsync = createAsyncThunk('updateUserAsync', async (user: userDto) => {
  try {      
    const response = await axios.post<userDto>('https://localhost:44346/user/UpdateAsync', user, { headers });
    return response.data;
  } catch (error) {
    toast("Bir hata oluştu.: " + error);
  }
}
)

export const deleteUserAsync = createAsyncThunk('deleteUserAsync', async (userId:string) => {
  try {
    const { data } = await axios.get<userDto[]>('https://localhost:44346/user/DeleteAsync/'+userId, { headers });
    return data;
  } catch (error) {
    toast("Bir hata oluştu.: " + error);
  }
}
)


export const getUserAsync = createAsyncThunk('getUserAsync', async () => {
  try {
    const { data } = await axios.get<userDto[]>('https://localhost:44346/user/GetAllAsync', { headers });
    return data;
  } catch (error) {
    toast("Bir hata oluştu.: " + error);
  }
}
)


export const userSlice = createSlice({
  name: 'user',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder.addCase(getUserAsync.fulfilled, (state, action) => {
      state = action.payload;
      return state;
    })
  }
})



export default userSlice.reducer