import axios from "axios";
import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import userDto from '../dtos/userDto';
import loginDto from '../dtos/loginDto';
import { toast } from 'react-toastify'

const headers = {
    'Content-Type': 'application/json',
    'Accept': 'application/json'
};

const initialState: userDto = null;


export const loginAsync = createAsyncThunk('loginAsync', async (data: loginDto) => {
    try {
        const response = await axios.post<userDto>('https://localhost:44346/login/LoginAsync', data);        
        return response.data;
    } catch (error) {         
        toast("Bir hata oluştu.: " + error);
    }
}
);



export const loginSlice = createSlice({
    name: 'login',
    initialState,
    reducers: {},
    extraReducers: (builder) => {
        builder.addCase(loginAsync.fulfilled, (state, action) => {
            state = action.payload;
            return state;
        })
    }
})

export default loginSlice.reducer