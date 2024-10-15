using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Lab2;

class FSM {
    
    private Elevator elevator1 = new Elevator();
    private Elevator elevator2 = new Elevator();
    

    public FSM() {

    }

    public void getState(int targetFloor) {
        bool isGoing1 = false;
        bool isGoing2 = false;
        bool isInRange1 = true;
        bool isInRange2 = true;
    

        isGoing1 = elevator1.getIsGoing();
        isGoing2 = elevator2.getIsGoing();

        isInRange1 = elevator1.isInRange(targetFloor);
        isInRange2 = elevator2.isInRange(targetFloor);

        return ;

    }

}
