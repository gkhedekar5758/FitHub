export interface RegisterUserDTO{
    email:string,
    password:string,
    firstName:string,
    lastName:string,
    isExternalProvider:boolean,
    isActive:boolean,
    userInfo:RegisterUserInfoDTO
}

export interface RegisterUserInfoDTO{
    height:number,
    weight:number,
    mobileNo:number,
    emergencyMobileNo:number,
    BMI:number
}