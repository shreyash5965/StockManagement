import { BehaviorSubject, Subject } from "rxjs";

export class SharedService {
    constructor(){}

    public productDetails:any = [];
    public subject = new Subject<any>();

    private messageSource = new BehaviorSubject(this.productDetails);
    currentMessage = this.messageSource.asObservable();

    changeMessage(message: string){
        this.messageSource.next(message);
    }
}
