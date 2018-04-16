class GroundRobot extends Phaser.Sprite {
  constructor(game, sf, x, y, image) {
    super(game, x, y, image);
    this.anchor.setTo(0.5);

    this.height = this.height/4 * sf;
    this.width = this.width/4 * sf;
    this.angle = -90;
    this.game.physics.enable(this);

    this.mySpeed = -(50/3)*sf;
    //console.log(this.body);
    this.body.velocity.y = -(50/3)*sf;
    this.oldVelocity = this.body.velocity;

    game.time.events.repeat(Phaser.Timer.SECOND * 2, 10, this.testRotate, this);

    this.game.stage.addChild(this);
  }
  keepGoing() {
    this.body.angularVelocity = 0;
    this.body.velocity = this.oldVelocity;
    this.game.physics.arcade.velocityFromAngle(this.angle, 10, this.body.velocity);
    //console.log('stopped ' + this.mySpeed);
  }
  testRotate() {
    console.log('rotating');
    this.oldVelocity = this.body.velocity;
    this.body.velocity = 0;

    this.body.angularVelocity = 20;

    //this.game.physics.arcade.velocityFromAngle(this.angle, 10, this.body.velocity);
    this.game.time.events.add(Phaser.Timer.SECOND * 0.5, this.keepGoing, this);
  }
}

export default GroundRobot;
