export interface UserModel {
  userId: string | null;
  firstName: string;
  lastName: string;
  email: string;
  username: string;
  phone: string;
  dateJoined: Date;
}

export interface UserRegistrationModel {
  email: string;
  password: string;
}