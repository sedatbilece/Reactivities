let data : any = 43;


data='43';


 interface Duck{
    name:string,
    numLegs:number,
    makeSound : (sound:string)=>void;
}

const duck1 : Duck = {
    name:'huey',
    numLegs:2,
    makeSound : (sound :any) => console.log(sound)
}

duck1.makeSound("test sound");

export default duck1;