
public interface ISelection 
{
    void SelectionControl();
    Unit GetUnit();
    bool GetRange();
    void AttackAnimTic(int damage);
    void AttackBool(bool ok);
    
}
