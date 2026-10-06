export interface Client {
  clientId: number;
  name: string;
  email: string;
  phone: string;
  address: string;
  createdAt: string;
}

export interface CreateClient {
  name: string;
  email: string;
  phone: string;
  address: string;
}

export interface UpdateClient {
  name?: string;
  email?: string;
  phone?: string;
  address?: string;
}
