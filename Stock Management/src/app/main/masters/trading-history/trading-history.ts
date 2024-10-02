export class TradingHistory {

    public static options = {
        year: "numeric",
        month: "2-digit",
        day: "numeric"
    } as const;

    public intstockid: number = 0
    public strstockname:string = "";
    public strshortname:string = "";
    public strdescription:string = "";
    public intlowestprice:string = "";
    public inthighestprice:string = "";
    public islisted:boolean = false;
    public dtelistedon:string = "";
}
