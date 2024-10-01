export class DatabaseUpdateDto{
    server: string
    databaseName: string
    userId: string
    password: string

    constructor(server: string, databaseName: string, userId: string, password: string) {
        this.server = server      
        this.databaseName = databaseName      
        this.userId = userId      
        this.password = password      
    }
}