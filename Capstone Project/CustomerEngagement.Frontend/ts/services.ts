export interface Ticket {
    ticketId: string;
    customerId: string;
    agentId: string;
    categoryId: number;
    title: string;
    description: string;
    status: TicketStatus;
    createdAt: string;
}

export enum TicketStatus {
    Open = 0,
    InProgress = 1,
    Resolved = 2
}

export interface Customer {
    customerId: string;
    fullName: string;
    email: string;
    phone: string;
}