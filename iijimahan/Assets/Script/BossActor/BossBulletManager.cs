using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletManager : MonoBehaviour
{
    public class BulletFactory
    {
        public GameObject Bullet;
        public float Radius;
        public string[] SpriteName;
        public Sprite[] BulletSprite;
        public bool ColliderType;
        float SizeX, SizeY;
        public BulletFactory(string[] sprite_name, bool collider_type, float radius = 0.5f, float size_x = 0.5f, float size_y = 0.5f)
        {
            Radius = radius;
            SpriteName = sprite_name;
            BulletSprite = new Sprite[SpriteName.Length];
            ColliderType = collider_type;
            SizeX = size_x;
            SizeY = size_y;
        }
        public void Load()
        {
            for (int i = 0; i < SpriteName.Length - 1; ++i)
            {
                BulletSprite[i] = GetSprite(SpriteName[0], SpriteName[i + 1]);
            }
        }
        //第一因数にはリソースフォルダからのスプライトファイルまでのパスをお願いします
        public static Sprite GetSprite(string fileName, string spriteName)
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>(fileName);
            return System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals(spriteName));
        }
        //自動追尾弾
        public void CreateBullet(Vector3 pos, int color)
        {
            GameObject newParent = new GameObject("Empty");
            Bullet = Instantiate(newParent, pos, Quaternion.identity);
            Bullet.tag = "EnemyBullet";
            SpriteRenderer sr = Bullet.AddComponent<SpriteRenderer>();
            sr.sprite = BulletSprite[color];
            sr.sortingLayerName = "BossBullet";
            Bullet.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            Bullet.AddComponent<BossBullet>();
            Bullet.AddComponent<BossPower>();
            Rigidbody rg = Bullet.AddComponent<Rigidbody>();
            rg.useGravity = false;
            if (ColliderType)
            {
                CapsuleCollider cc = Bullet.AddComponent<CapsuleCollider>();//.radius = SizeX;
                cc.radius = Radius;
                cc.isTrigger = true;
            }
            else
            {
                BoxCollider bc = Bullet.AddComponent<BoxCollider>();//.size = new Vector2(SizeX, SizeY);
                bc.size = new Vector2(SizeX, SizeY);
                bc.isTrigger = true;
            }
            Destroy(newParent);
        }
        //ランダム攻撃弾
        public void CreateBullet2(Vector3 pos, int color)
        {
            GameObject newParent = new GameObject("Empty");
            Bullet = Instantiate(newParent, pos, Quaternion.identity);
            Bullet.tag = "EnemyBullet";
            SpriteRenderer sr = Bullet.AddComponent<SpriteRenderer>();
            sr.sprite = BulletSprite[color];
            sr.sortingLayerName = "BossBullet";
            Bullet.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            Bullet.AddComponent<BossBullet2>();
            Bullet.AddComponent<BossPower>();
            Rigidbody rg = Bullet.AddComponent<Rigidbody>();
            rg.useGravity = false;
            if (ColliderType)
            {
                CapsuleCollider cc = Bullet.AddComponent<CapsuleCollider>();//.radius = SizeX;
                cc.radius = Radius;
                cc.isTrigger = true;
            }
            else
            {
                BoxCollider bc = Bullet.AddComponent<BoxCollider>();//.size = new Vector2(SizeX, SizeY);
                bc.size = new Vector2(SizeX, SizeY);
                bc.isTrigger = true;
            }
            Destroy(newParent);
        }
        public void CreateBullet3(Vector3 pos, int color)
        {
            GameObject newParent = new GameObject("Empty");
            Bullet = Instantiate(newParent, pos, Quaternion.identity);
           // Bullet.tag = "Charge";
            SpriteRenderer sr = Bullet.AddComponent<SpriteRenderer>();
            sr.sprite = BulletSprite[color];
            sr.sortingLayerName = "BossBullet";
            Bullet.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
            Bullet.AddComponent<BossLaser>();
            Bullet.AddComponent<BossPower>();
            Rigidbody rg = Bullet.AddComponent<Rigidbody>();
            rg.useGravity = false;
            BoxCollider bc = Bullet.AddComponent<BoxCollider>();//.size = new Vector2(SizeX, SizeY);
            SizeX =0.0f;
            SizeY =0.0f;
            bc.size = new Vector2(SizeX, SizeY);
            bc.isTrigger = true;

            Destroy(newParent);
        }

        public void CreateBullet4(Vector3 pos, int color)
        {
            GameObject newParent = new GameObject("Empty");
            Bullet = Instantiate(newParent, pos, Quaternion.identity);
            Bullet.tag = "EnemyBullet";
            SpriteRenderer sr = Bullet.AddComponent<SpriteRenderer>();
            sr.sprite = BulletSprite[color];
            sr.sortingLayerName = "BossBullet";
            Bullet.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            Bullet.AddComponent<BossBulletFrind2>();
            Bullet.AddComponent<BossPower>();
            Rigidbody rg = Bullet.AddComponent<Rigidbody>();
            rg.useGravity = false;
            if (ColliderType)
            {
                CapsuleCollider cc = Bullet.AddComponent<CapsuleCollider>();//.radius = SizeX;
                cc.radius = Radius;
                cc.isTrigger = true;
            }
            else
            {
                BoxCollider bc = Bullet.AddComponent<BoxCollider>();//.size = new Vector2(SizeX, SizeY);
                bc.size = new Vector2(SizeX, SizeY);
                bc.isTrigger = true;
            }
            Destroy(newParent);
        }
        //エリア封鎖弾
        public void CreateBullet5(Vector3 pos, int color)
        {
            GameObject newParent = new GameObject("Empty");
            Bullet = Instantiate(newParent, pos, Quaternion.identity);
            Bullet.tag = "EnemyBullet";
            SpriteRenderer sr = Bullet.AddComponent<SpriteRenderer>();
            sr.sprite = BulletSprite[color];
            sr.sortingLayerName = "BossBullet";
            Bullet.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            Bullet.AddComponent<BossBullet3>();
            Bullet.AddComponent<BossPower>();
            Rigidbody rg = Bullet.AddComponent<Rigidbody>();
            rg.useGravity = false;
            if (ColliderType)
            {
                CapsuleCollider cc = Bullet.AddComponent<CapsuleCollider>();//.radius = SizeX;
                cc.radius = Radius;
                cc.isTrigger = true;
            }
            else
            {
                BoxCollider bc = Bullet.AddComponent<BoxCollider>();//.size = new Vector2(SizeX, SizeY);
                bc.size = new Vector2(SizeX, SizeY);
                bc.isTrigger = true;
            }
            Destroy(newParent);
        }
        public void CreateBullet6(Vector3 pos, int color)
        {
            GameObject newParent = new GameObject("Empty");
            Bullet = Instantiate(newParent, pos, Quaternion.identity);
          //  Bullet.tag = "Charge";
            SpriteRenderer sr = Bullet.AddComponent<SpriteRenderer>();
            sr.sprite = BulletSprite[color];
            sr.sortingLayerName = "BossBullet";
            Bullet.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            Bullet.AddComponent<BossSuperLaser>();
            Bullet.AddComponent<BossPower>();
            Rigidbody rg = Bullet.AddComponent<Rigidbody>();
            rg.useGravity = false;
            BoxCollider bc = Bullet.AddComponent<BoxCollider>();//.size = new Vector2(SizeX, SizeY);
            SizeX = 0.0f;
            SizeY = 0.0f;
            bc.size = new Vector2(SizeX, SizeY);
            bc.isTrigger = true;

            Destroy(newParent);
        }
        public void CreateBullet7(Vector3 pos, int color)
        {
            GameObject newParent = new GameObject("Empty");
            Bullet = Instantiate(newParent, pos, Quaternion.identity);
            //  Bullet.tag = "Charge";
            //SpriteRenderer sr = Bullet.AddComponent<SpriteRenderer>();
            //sr.sprite = BulletSprite[color];
            //sr.sortingLayerName = "BossBullet";
            Bullet.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            Bullet.AddComponent<BossLaser2>();
            Bullet.AddComponent<BossPower>();
            Rigidbody rg = Bullet.AddComponent<Rigidbody>();
            rg.useGravity = false;
            BoxCollider bc = Bullet.AddComponent<BoxCollider>();//.size = new Vector2(SizeX, SizeY);
            SizeX = 0.0f;
            SizeY = 0.0f;
            bc.size = new Vector2(SizeX, SizeY);
            bc.isTrigger = true;

            Destroy(newParent);
        }
        public void CreateBullet7Up(Vector3 pos, int color)
        {
            GameObject newParent = new GameObject("Empty");
            Bullet = Instantiate(newParent, pos, Quaternion.identity);
            //  Bullet.tag = "Charge";
            //SpriteRenderer sr = Bullet.AddComponent<SpriteRenderer>();
            //sr.sprite = BulletSprite[color];
            //sr.sortingLayerName = "BossBullet";
            Bullet.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            Bullet.AddComponent<BossLaser2Up>();
            Bullet.AddComponent<BossPower>();
            Rigidbody rg = Bullet.AddComponent<Rigidbody>();
            rg.useGravity = false;
            BoxCollider bc = Bullet.AddComponent<BoxCollider>();//.size = new Vector2(SizeX, SizeY);
            SizeX = 0.0f;
            SizeY = 0.0f;
            bc.size = new Vector2(SizeX, SizeY);
            bc.isTrigger = true;

            Destroy(newParent);
        }
        public void CreateBullet7Down(Vector3 pos, int color)
        {
            GameObject newParent = new GameObject("Empty");
            Bullet = Instantiate(newParent, pos, Quaternion.identity);
            //  Bullet.tag = "Charge";
            //SpriteRenderer sr = Bullet.AddComponent<SpriteRenderer>();
            //sr.sprite = BulletSprite[color];
            //sr.sortingLayerName = "BossBullet";
            Bullet.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            Bullet.AddComponent<BossLaser2Down>();
            Bullet.AddComponent<BossPower>();
            Rigidbody rg = Bullet.AddComponent<Rigidbody>();
            rg.useGravity = false;
            BoxCollider bc = Bullet.AddComponent<BoxCollider>();//.size = new Vector2(SizeX, SizeY);
            SizeX = 0.0f;
            SizeY = 0.0f;
            bc.size = new Vector2(SizeX, SizeY);
            bc.isTrigger = true;

            Destroy(newParent);
        }
    }
    public BulletFactory[] FBulletFactory = new BulletFactory[]
    {
            new BulletFactory(new string[]{ "img/bullet/b0","b0_0","b0_1","b0_2","b0_3","b0_4" }, true),
    };
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < FBulletFactory.Length; ++i)
        {
            FBulletFactory[i].Load();
        }
    }
}
