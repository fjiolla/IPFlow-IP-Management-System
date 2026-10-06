export interface Trademark {
  trademarkId: number;
  applicationNumber: string;
  name: string;
  description: string;
  registrationDate: string;
  renewalDate?: string;
  status: string;
  classNumber: string;
  clientId: number;
  clientName: string;
  lawyerId?: number;
  lawyerName?: string;
  createdAt: string;
}

export interface CreateTrademark {
  applicationNumber: string;
  name: string;
  description: string;
  registrationDate: string;
  renewalDate?: string;
  status: string;
  classNumber: string;
  clientId: number;
  lawyerId?: number;
}

export interface UpdateTrademark {
  name?: string;
  description?: string;
  registrationDate?: string;
  renewalDate?: string;
  status?: string;
  classNumber?: string;
  lawyerId?: number;
}
