export interface IUser{
  userID:number,
  email:string,
  password:string,
  firstName:string,
  lastName:string
  userInfo:IUserInfo
}

export interface IUserInfo{
  height:number,
  weight:number,
  mobileNo:string,
  emergencyMobileNo:string,
  bmi:number
}
