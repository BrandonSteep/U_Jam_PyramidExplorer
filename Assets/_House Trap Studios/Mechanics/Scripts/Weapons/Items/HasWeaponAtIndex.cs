public class HasWeaponAtIndex
{
    private int index { get; set; }
    private bool weaponExists { get; set; }


    public HasWeaponAtIndex(int i, bool b){
        index = i;
        weaponExists = b;
    }

    public int GetIndex(){
        return index;
    }
    public bool HasWeapon(){
        return weaponExists;
    }
}
