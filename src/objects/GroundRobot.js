class GroundRobot extends Phaser.Sprite {
  constructor(game, sf, angle, x, y, image) {
    super(game, x, y, image);
    this.anchor.setTo(0.5);

    this.count = 0;

    this.height = this.height/4 * sf;
    this.width = this.width/4 * sf;
    //this.angle = -90;
    this.angle = angle;
    this.smoothed = false;

    this.game.physics.p2.enableBody(this);
    this.body.setCircle(10);
    this.body.fixedRotation = true;
    // this.body.velocity.x = 5;
    //this.enableBody = true;

    this.mySpeed = -(50/3)*sf;
    console.log(this.angle + " " + Math.sin(this.angle*Math.PI/180) + " " + Math.cos(this.angle*Math.PI/180));
    this.body.velocity.y = (50/3)*sf*Math.sin(this.angle*Math.PI/180);
    this.body.velocity.x = (50/3)*sf*Math.cos(this.angle*Math.PI/180);
    //this.game.physics.p2.velocityFromAngle(this.angle, 10, this.body.velocity);
    //
    // this.oldVelocity = this.body.velocity;
    // this.body.collideWorldBounds = true;
    //
    // this.game.time.events.repeat(Phaser.Timer.SECOND * 5, 50, this.spin, this);
    this.game.stage.addChild(this);
  }
  update() {
    //this.body.setZeroVelocity();
    //this.body.moveLeft(20);
  //   //Math.abs(-4);
  //   if(Math.abs(this.x) > this.bounds.x) {
  //     console.log('out of bounds');
  //     this.destroy();
  //   }
  //   if(Math.abs(this.y) > this.bounds.y) {
  //     if(this.y < 0) {
  //       console.log('points scored');
  //     }
  //     this.destroy();
  //   }
  //   //console.log(this.x);
  }
  keepGoing() {
    this.body.angularVelocity = 0;
    this.body.velocity = this.oldVelocity;
    this.game.physics.arcade.velocityFromAngle(this.angle, 10, this.body.velocity);
    console.log("x: " + this.x + " y: " + this.y);
  }
  spin() {
    var angle = Math.floor(Math.random() * 21);
    if(this.count == 4) {
      angle = 180;
      this.count = 0;
    } else {
      this.count++;
    }
    this.oldVelocity = this.body.velocity;
    this.body.velocity = 0;

    this.body.angularVelocity = 90; //degrees/sec
    var time = angle/90;
    this.game.time.events.add(Phaser.Timer.SECOND * time, this.keepGoing, this);
  }
}

export default GroundRobot;
