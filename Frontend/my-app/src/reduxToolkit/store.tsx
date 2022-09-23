
import { configureStore} from "@reduxjs/toolkit";
import { useDispatch,TypedUseSelectorHook,useSelector } from 'react-redux';
import userSlice from '../reduxToolkit/userSlice';
import loginSlice from '../reduxToolkit/loginSlice';
import departmentSlice from '../reduxToolkit/departmentSlice';

const store = configureStore({
    reducer:{        
        user:userSlice,
        login:loginSlice,
        department:departmentSlice,
    },
});

export default store;

export type RootState = ReturnType <typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export const useAppDispatch = () => useDispatch<AppDispatch>();
export const useAppSelector :TypedUseSelectorHook<RootState>  = useSelector;
