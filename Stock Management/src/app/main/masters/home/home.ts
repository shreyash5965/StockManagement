export class Home {

    public static options = {
        year: "numeric",
        month: "2-digit",
        day: "numeric"
    } as const;

    public intuserid: number = 0
    public strusername:string = "";
    public strpassword:string = "";
    public strfirstname:string = "";
    public strmiddlename:string = "";
    public strlastname:string = "";
    public dtedob:string = new Date().toLocaleDateString("fr-CA",Home.options);
    public stremailid:string = "";
    public strcontactno:string = "";
}
