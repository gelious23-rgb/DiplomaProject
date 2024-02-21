using UnityEngine;

namespace Script.Card
{

    public class CharacterCard
    {
        public string Name;
        public string Type;
        public string Description;
        public int Damage;
        public int Manacost;
        public int Hp;
        public readonly Sprite Image;
        public bool IsPlaced;
        public bool CanAttack;

        public bool IsAlive => Hp > 0;

        public CharacterCard(string name,string type, string description, string spritePath, int damage, int manacost, int hp)
        {
            Name = name;
            Type = type;
            Description = description;
            Image = Resources.Load<Sprite>(spritePath);
            Damage = damage;
            Manacost = manacost;
            Hp = hp;
            CanAttack = false;
            IsPlaced = false;
        }

        public void ChangeAttackState(bool canAttack)
        {
            CanAttack = canAttack;
        }

        public void GetDamage(int dmg)
        {
            Hp -= dmg;
        }
    }
}